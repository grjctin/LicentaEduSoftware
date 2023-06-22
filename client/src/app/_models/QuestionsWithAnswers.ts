import { Answer } from "./answer";
import { Question } from "./question";

export interface QuestionsWithAnswers {
    questions: Question[];
    answers: Answer[];
}