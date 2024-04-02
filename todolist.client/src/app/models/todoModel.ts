import { SubTodoModel } from "./subTodoModel";

export interface Todo {
  id: number;
  task: string;
  deadline: Date;
  isCompleted: boolean;
  subTodos: SubTodoModel[];
  moreDetails?: string;
  showDetails?: boolean;
}
