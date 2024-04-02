export class TodoCreationModel {
  constructor() { }
  public id: number = 0;
  public task: string = "";
  public deadline: Date = new Date();
  public moreDetails: string = "";

}
