# DlibFaceRecognition

## How To Run
#### First, make sure that you have build the solution properly.
#### Note that you need to have .NET5 installed on your device.
#### Hit your IDE Run button; if you faced an error talking about Microsoft package and your framework version, ignore it and use this command instead, to run your application:
```bash
dotnet run <First_Image_Directory> <Second_Image_Directory> <...>
```

```bash
Output : number of people found in images: <a number>
```

#### Note that faces that belong to on person, will be ignored after first detection. So the application will ignore repeated persons.
#### MAKE SURE!! that you have downloaded, extracted, and added these files to your <Project_dir>
- http://dlib.net/files/shape_predictor_5_face_landmarks.dat.bz2
- http://dlib.net/files/dlib_face_recognition_resnet_model_v1.dat.bz2
