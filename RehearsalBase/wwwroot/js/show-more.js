//there is error here

//const btnShowMore = document.querySelector('#show-more');
//const items = document.querySelectorAll('.time')
//const itemsLength = items.length;
//let visible = 5;

//btnShowMore.addEventListener('click', () => {
//    visible += 1; //step
//    for (let i = 0; i < visible; i++) {
//        items[i].style.display = 'block';
//    }
//});
const btnShowMore = document.querySelector('#show-more');
const items = document.querySelectorAll('.time')
const itemsLength = items.length;
let visible = 5;

btnShowMore.addEventListener('click', () => {
    visible += 5;
    for (let i = 0; i < visible && i < itemsLength; i++) {
        items[i].style.display = 'block';
    }
    if (visible >= itemsLength) {
        btnShowMore.style.display = 'none';
    }
});
//document.addEventListener('DOMContentLoaded', () => function () {
    
//}