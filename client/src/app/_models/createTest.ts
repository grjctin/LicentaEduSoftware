import { TestQuestion } from "./testQuestion";

export interface CreateTest {
    questionConfigurations: TestQuestion[];
    studentIds: number[];
    startDate: Date;
    duration: number;
}