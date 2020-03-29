# LINQ-Processing-at-Runtime

## Purpose
This project is a showcase to write and run LINQ query at runtime.
### Features:
- Input LINQ query via web interface
- Execute against in-memory dataset
- View output via Grid or bar chart

## Requires
- .Net Core SDK 3.1
- Visual Studio 2019

## Guide
- Run the app
- Get sample query: https://localhost:44340/Home/SampleQuery
- Input sample query to text area on Index page then click Submit.
- View result by clicking Grid or Chart button.
- View source data: https://localhost:44340/Home/Data

## Limitation
- The compile engine supports most of feature of LINQ im-memory query. However, I have only tested several simple cases, so you need to write your own test to check where this tool supports your cases.
- One limitation the I have found is related to null propagator. This does support check null but with customized function. Please see Sample Query 6 for detail.

## Credits
The core of project: compile engine was taken from https://github.com/gadagrj/TextToLINQ with small updates.
