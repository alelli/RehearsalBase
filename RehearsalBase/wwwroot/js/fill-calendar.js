
const dateNow = new Date();
let lastDate = new Date();
lastDate.setDate(dateNow.getDate() + 27); // 9-10 - 20-21

let date = new Date();
while (date.getDay() != 1) {
    date.setDate(date.getDate() - 1);
}

let result = "";
for (let i = 0; i < 4; i++) {
    result += "<tr>";
    for (let j = 0; j < 7 && date <= lastDate; j++) {
        if (dateNow > date) {
            result += `<td><a class="empty">${date.getDate()}</a></td>`;
        }
        else
            result += `<td><a href="#">${date.getDate()}</a></td>`;
        date.setDate(date.getDate() + 1);
    }
    result += "</tr>"
}
document.querySelector(".calendar tbody").innerHTML = result;
