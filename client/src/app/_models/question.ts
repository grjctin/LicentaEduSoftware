export interface Question {
    id: number;
    categoryId: number;
    difficulty: number;
    text: string;
    answerType: number;
    correctAnswer?: string
    answer2?: string;
    answer3?: string;
    answer4?: string;
  }