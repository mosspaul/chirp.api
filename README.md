# chirp.api

## Purpose
Chirp, the premier individual resource planner! Chirp will allow users to schedule tasks and plan for events, budget and manage finances, set fitness goals and manage studies and hobbies. 


## Roadmap
- ### **V1:** Launch Chirp, basic user authentication for primarily budgeting

    **Features:**
    - _Track income and expenses_ 
    - _Set monthly limits (siloed)_ 
    - _Bill and service management_
    - _Basic visualizations_

    **Platforms:**
    - Android
    - Linux
    - Windows
    - Web?

- ### **V2:** Event management and dynamic calendaring

    **Features:**
    - _Create daily, weekly and monthly plans_ 
    - _Notifications of upcoming events_
    - _Different event types for time management_

    **Platforms:**
    - Android
    - IOS
    - Linux
    - Windows
    - Web

- ### **V3:** Goalsetting for fitness, personal and academics

    **Features:**
    - _Create goals trackable along events_ 
    - _Special fitness tracking (hooking into wearables/native APIs)_ 

    **Platforms:**
    - Android
    - IOS
    - Linux
    - Windows
    - Web

## Setup Instructions

1. Cloned from git
2. Added solution file `dotnet new sln chirp.api`
3. Created subprojects ```dotnet new webapi -n api && dotnet new classlib -n core```
4. Added projects to chirp.api solution `dotnet sln add api/api.csproj`
5. Added references between projects `dotnet add api reference data/data.csproj`
6. Added VSCode tasks and build file

## Next Steps
- Code First db with EF Migrations
- Install Nuget Packages for EF and Npgsql
- Add connection string to AppSettings



