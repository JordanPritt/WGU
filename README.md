WGU
===

This repository houses the various projects required to pass the WGU Bachelors of Science in Software Developemnt program. The various applications in here serve as a demonstration of using the C# programming language with .NET Framework, Core, and Xamarin Forms.

## Teacher Assistant
![.NET Core Build](https://github.com/JordanPritt/WGU/workflows/.NET%20Core%20Build/badge.svg)

Capstone project built to serve as a tool for teachers to use in managing their class rooms. This application runs off .NET core and hosted with Azure, GitHub, and managed CI/CD with GitHub Actions. Data is controlled via code-first with Entity Framework core, and hosted on an AzureSQL servless instance. Application logic lives in service classes and is seperated from the controller's actions and views for better separation of concerns. Unit testing is built with xunit.

## Term Manager
A mobile application for managing a student's term information. Built using the MVVM design pattern with Xamarin.Forms for cross platform functionality in both iOS and Andriod devices. Also Utilizes SQLite for data persistance on mobile device's file system with mapping from Entity Framework core. Also using MS Test for unit testing to ensure application logic runs accordingly.

## Scheduler
A desktop based system for processing appointments within an organization. Utilizes MySQL for data persistence, Entity Framework for handling data-layer in OOP, and seperation of concerns with DTO, Models, and Service classes.