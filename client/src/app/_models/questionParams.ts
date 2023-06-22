export class QuestionParams {
    categoryId = 0;
    difficulty = 4; //1, 2, 3 pentru a returna doar anumite dificultati
    //cazul difficulty=4 este default iar acesta va returna toate intrebarile, la care se va aplica orderBy, pageNumber si pageSize
    answerType = 0; //1, 2
    pageNumber = 1;
    pageSize = 10;
    orderBy = 'dateAdded'; //dateAdded, difDescending, difAscending
}