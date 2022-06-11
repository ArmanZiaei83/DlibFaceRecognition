# DlibFaceRecognition

## About
#### DlibFaceReconition is a sample of using <a href="https://github.com/takuya-takeuchi/DlibDotNet">DlibDotNet</a> library. It gets you a list of faces, and the output would be the unrepeated number of faces.

## How To Run
#### Make sure that you have .NET5 installed on your OS.

#### First, build the project:
```bash
dotnet build
```
#### Then, use this command to run the application:
```bash
dotnet run <First_Image_Directory> <Second_Image_Directory> <...>
```

```bash
Output : number of people found in images: <number of unrepeated faces in the list given>
```
