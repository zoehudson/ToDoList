export class SubTodoModel {
  constructor() { }
  public task: string = "";
  public deadline: Date = new Date();
  public moreDetails: string = "";
  public id: number = 0;
  public parentId: number = 0;
  public isCompleted: boolean = false;
  public showDetails?: boolean = false;
}
