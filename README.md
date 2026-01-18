# LicentaEduSoftware
[Full project documentation](./Documentatie.pdf)

This is a project that will allow teachers:
- To have a more efficient overview of their students level(each student will have the grades as we know them and some grades for each particular category of a subject, indicating the students level in that particular subdomain)
- Generate custom tests for each student in a short time using a large question bank.
- Grade the tests after they have been taken by the students and automatically update their grades for each particular subject.
At the request of the coordinator teacher I also included a student role that will be able to take the tests online, but this is not the real purpose of the app.
- Add questions and edit the question bank, it includes pagination, sorting, filtering and even some client-side caching.
- Track absence of their students


The app is not 100% finished but the main features are. My focus for this app was the 'teacher' side of it, I wanted it to be a tool for them.
Credentials for using the app are username: profesor, password: profesor. The main features are visible in the classes component on the student grades tab. I didn't really focus on the design of the app, the functionalities were more important at that time.

What I learned

Backend (.NET / Web API)
- Designing and implementing RESTful APIs
- Working with asynchronous programming (async / await) to handle non-blocking requests
- Using Entity Framework (ORM) for database access and object–relational mapping
- Designing a relational database schema with multiple entities and relationships
- Implementing the Repository Pattern to separate data access logic from business logic
- Applying pagination on the server side to efficiently handle large datasets
- Implementing filtering and sorting logic on API endpoints
- Structuring projects using clean architecture principles
- Understanding and applying DTOs for data transfer
- Basic login and password storage
- Basic error handling and validation

Frontend(Angular)
- Building a component-based architecture
- Using services to centralize business logic and API communication
- Consuming REST APIs using HttpClient
- Managing state and data flow between components
- Implementing client-side pagination, filtering, and sorting
- Implementing client-side caching to avoid unnecessary API requests
- Understanding component lifecycle and service lifetimes
- Using two-way data binding
- Creating reactive forms for data input and validation
- Optimizing performance by updating only affected components

- Designing a client–server architecture
- Understanding how frontend and backend communicate over HTTP
- Structuring a scalable and maintainable full-stack application
- Debugging and testing across frontend and backend
- Working with environment-based configuration

I intend on updating the app to the latest Angular and .Net versions and add the last functionalities for the app to be complete.
- The option to add photos for some questions/answers if needed
- Exporting tests as pdfs so they can be printed
- Updating the database from SQLite to something better
- Adding the student role using .NET identity



