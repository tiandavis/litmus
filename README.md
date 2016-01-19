# Automated Gmail Screenshot Processing
## With automated upload to Amazon S3 and recording in Screenshots table using Dapper

![Gmail](http://i.imgur.com/SPpCF9V.png)

## Screenshots Table
![Gmail](http://i.imgur.com/N98dLIM.png)

## S3 Keys
- https://litmus-interview.s3.amazonaws.com/201601190047455038/screenshot.png
- https://litmus-interview.s3.amazonaws.com/201601190048084498/screenshot.png
- https://litmus-interview.s3.amazonaws.com/201601190048396997/screenshot.png


# Running Options

## Command Line (With Arguments)

![Command Line 1](http://i.imgur.com/0X1Ajzq.png)

Usage: Litmus.exe `<Username>` `<Password>`
`<Username>` and `<Password>` for your Gmail account. No quotes needed around `<Username>` or `<Password>`.

Grab your credentials and run .\Litmus.exe `<Username>` `<Password>`.

## Command Line (Without Arguments)

![Command Line 2](http://i.imgur.com/lV5p2F6.png)

To enter your Gmail credentials directly, run Litmus.exe without command line arguments.

Enter Gmail Username:
`<Username>`

Enter Gmail Password:
`<Password>`


# Unit Test Suite

The test suite use NUnit and NUnitLite Test Runner.

![Test Runner](http://i.imgur.com/rMeaf09.png)

# Option 1: Visual Studio

Select Litmus.Tests project and Start/F5. NUnitLite Test Runner will automatically run the test suite.

# Option 2: Test Runner from Command Line

Usage: Litmus.Test.exe

No command line options, just run .\Litmus.Test.exe to kickoff the test runner.


# Configuration and Building

You must enter your configuratiion keys before running the application for the first time. The configurations are in \Litmus.Services\App.config. The available configurations are as follows:

- AWSAccessKey - Your Amazon S3 access key
- AWSSecretKey - Your Amazon S3 secret key
- BucketName - Your Amazon S3 bucket name
- FileName - Gmail screenshot image will have this name
- Username - Gmail username
- Password - Gmail Password
- MySQL - Connection string for your MySQL database instance

The application was built with Visual Studio 2015. You can download the Visual Studio 2015 Community Edition for free, open Litmus.sln and build the solution with Start/F5.

