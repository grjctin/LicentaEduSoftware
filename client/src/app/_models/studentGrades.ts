export interface StudentsGrades {
    id: number;
    firstName: string;
    lastName: string;
    grades: number[];
    categoryGrades: { [categoryId: number]: number[] };
  }