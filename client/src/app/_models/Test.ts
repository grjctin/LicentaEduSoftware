import { QuizQuestion } from "./QuizQuestion";

export interface Test {
    studentName: string;
    startDate: string;
    duration: number;
    questions: QuizQuestion [];
}