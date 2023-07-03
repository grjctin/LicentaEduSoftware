export interface TestCorrect {
    testId: number;
    answers: {
        questionNumber: number;
        givenAnswer: string; 
    }[];
}