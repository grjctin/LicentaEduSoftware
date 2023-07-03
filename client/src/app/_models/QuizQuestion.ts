import { Answer } from "./answer";

export interface QuizQuestion {
    questionId: number;
    questionText: string;
    answerType: number;
    questionNumber: number;
    correctAnswer: number;
    answers: string [];    
}