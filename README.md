# Project Name

This is the Coding Exercise for Tx Training Program

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/your-username/project-name.git](https://github.com/Jav4nsJuice/scheduling-api.git
    ```

2. **Open the Solution:**

    Open the project solution file (`Truextend.Scheduling.sln`) in Visual Studio Community. 

3. **Restore NuGet Packages:**

    Visual Studio should automatically restore the NuGet packages when you open the solution. However, if it doesn't, you can manually restore them:

    - Right-click on the solution in the Solution Explorer.
    - Select "Restore NuGet Packages".

4. **Build the Project:**

    Build the project by selecting "Build > Build Solution" from the menu, or by pressing `Ctrl + Shift + B`. We used SQL Server 2019 dockerized for Mac. And created the DB structure with the running of migrations

5. **Run the Application:**

    Set the Presentation startup project and run the application by pressing `F5` or selecting "Debug > Start Debugging" from the menu.

## Usage

This project has 4 pages:
1. Home Page giving a Image Carousel and also the top Students and Courses, in which the Top Students are the ones with the most attended courses and the top courses are the ones who have more students inscripted.
2. StudentCourse table with filter and pagination and also has a Add Resource button that adds a Student to a Course.
3. Students Page with CRUD operations and filter.
4. Courses Page with CRUD operations and filter. 

## Contributing

1. Fork the repository.
2. Creating branches (`US/S-TicketNumber-short-description`).
3. Commiting your changes (`git commit -m 'US/S-TicketNumber: Description in past.'`).
4. Push to the branch (`git push origin branch-name`).

## License

Tx-SCheduling © 2024 Javier Alejandro Ferrel Rivera
