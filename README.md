# ToDo List Application

This is a simple ToDo List application built with .NET Core and Angular.

## Requirements

- .NET Core 8
- Angular 18
- Node.js (for Angular development)
- PrimeNG (set up documentation here:https://primeng.org/installation )

## Installation

### Backend (.NET Core)

1. Clone this repository to your local machine.
2. Navigate to the `backend` directory.
3. Run the following command to restore the .NET Core dependencies:
   dotnet restore
   run the following installs
npm install -g @angular/cli
npm install
npm install primeng

4. Add PrimeNG styles to your project. You can either import them in `angular.json` or `src/styles.css`:

With angular.json:
"styles": [
"node_modules/primeng/resources/themes/lara-light-blue/theme.css",
"node_modules/primeng/resources/primeng.min.css",
...
]


With styles.css:
@import "primeng/resources/themes/lara-light-blue/theme.css";
@import "primeng/resources/primeng.css";

ng serve


The frontend will be served at `http://localhost:4200`.

## Usage

Once both the backend and frontend servers are running, you can access the ToDo List application by visiting `http://localhost:4200` in your web browser.

## Features

- Add, delete, and update todos.
- Add, delete, and update subtodos for each todo.
- Mark todos and subtodos as completed.
- View and edit todo details.

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues if you encounter any problems or have suggestions for improvements.

## License

This project is licensed under the [MIT License](LICENSE).

