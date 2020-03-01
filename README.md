![MSBuild](https://github.com/KMRNicholson/nsld/workflows/MSBuild/badge.svg)

# Never Skip Leg Day Mobile
Mobile application for my Never Skip Leg Day web app which can be found here at https://neverskiplegday.ca. The user interface and experience will be revamped from the old version currently available online.

## Purpose
The purpose of this application is to create an app that gives the user the capability to create their own specialized workout programs, as well as track the progress of their workouts. On top of the workout tracking feature, the app also has meal tracking as part of the nutritition feature, as well as personal records tracking as part of the records feature.

Finally, this is a personal project that will help my own understanding of mobile development. I chose Xamarin.Forms as I am comfortable using C# as a development language, as well as my familiarity with the .NET Standard library.

## Features
The features included in this app are workouts, nutrition, and records. The app will require the user to sign in or sign up. When the user signs in, they will have access to more features such as the records feature, as well as account settings. To make logging in and signing up easier for the user, I plan to implement social media log in and sign up on top of the manual sign up by email and password. 

### Prototype
If you are interested in viewing the prototype, first download and install [Adobe XD](https://www.adobe.com/products/xd.html). Once that is done, download the .xd file from the prototype directory [here](https://github.com/KMRNicholson/nsld/blob/master/prototype/nsld_prototype.xd). Open the .xd file with Adobe XD and click on the play button in the upper right hand corner.

### Workouts
The workouts feature gives the user the following functionality:
  - Create workout sessions.
  - Add exercises to workout sessions.
  - Track exercise progress by weight, sets, and reps.
  - Modify workouts and exercises.
  - Remove workouts and exercises.
    
### Nutrition
The nutritition section of the application gives the user the ability to:
  - Create meals for today's meal plan.
  - Add food to a meal.
  - Optionally track food and meals by protein, carbs, fats, and calories.
  - Modify food and meals.
  - Remove food and meals.
  - (Not in the prototype)Macronutrient proportion for food, meals and today's total will be visualized in a pie chart.
  
### Records
The Records feature allows the user to:
  - Create an exercise to enter a personal record lift.
  - For the selected exercise, the user can enter a personal record by weight and reps.
  - Add many records for one lift.
  - Records will be tracked by date.
  - (Not in the prototype)Record progression will be visualized as a graph.
  - Modify existing records and exercises.
  - Remove records and exercises.
