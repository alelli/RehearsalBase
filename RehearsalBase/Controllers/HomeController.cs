using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RehearsalBase.Models;
using System.Linq;

namespace RehearsalBase.Controllers
{
    public class HomeController : Controller
    {
        public readonly PostgreDbContext _context;

        public HomeController(PostgreDbContext context)
        {
            _context = context;
        } 

        public async Task<IActionResult> Index()
        {
            DateTime now = DateTime.Now;
            DateTime lastDate = DateTime.Now.Date.AddDays(10); //21;
            var freeTime = new List<RehearsalTime>();
            for (DateTime i = now; i <= lastDate; i = i.AddDays(1))
            {
                freeTime.Add(new RehearsalTime() { Date = new DateOnly(i.Year, i.Month, i.Day) });
            }

            var rehearsalGroups =
                (from r in _context.Rehearsals
                where r.RehearsalDate >= new DateOnly(now.Year, now.Month, now.Day) &&
                r.RehearsalDate <= new DateOnly(lastDate.Year, lastDate.Month, lastDate.Day)
                orderby r
                select r)
                .GroupBy(p => p.RehearsalDate);

            var rehearsalTime = new List<RehearsalTime>();
            if (rehearsalGroups != null)
            {
                foreach (var group in rehearsalGroups)
                {
                    rehearsalTime.Add(new RehearsalTime() { Date = group.Key, Time = new List<TimeOnly>() });
                    foreach (var rehearsal in group)
                    {
                        rehearsalTime.Last().Time.Add(rehearsal.RehearsalStart);
                    }
                }
            }

            foreach (var i in rehearsalTime)
            {
                foreach (var item in i.Time)
                {
                    freeTime.First(d => d.Date == i.Date).Time.Remove(item);
                }
            }
            return View(freeTime);
        }

        public async Task<ActionResult<List<Rehearsal>>> CheckData(List<string> dateTimes, int rehearsalCategory)
        {
            var rehList = new List<Rehearsal>();
            for (int i = 0; i < dateTimes.Count; i++)
            {
                DateTime dt = DateTime.Parse(dateTimes[i]);
                TimeOnly t = TimeOnly.FromDateTime(dt);
                var newRehearsal = new Rehearsal()
                {
                    RehearsalDate = DateOnly.FromDateTime(dt),
                    RehearsalStart = t,
                    RehearsalEnd = t.AddHours(1),
                    Category = rehearsalCategory,
                    Customer = 1
                };
                rehList.Add(newRehearsal);
                //_context.Rehearsals.Add(newRehearsal);
            }
            //await _context.SaveChangesAsync();
            return View(rehList);
        }


        public async void Save(Rehearsal newRehearsal)
        {

        }

    }
}
