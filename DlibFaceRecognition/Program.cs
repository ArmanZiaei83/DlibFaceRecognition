#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet;
using DlibDotNet.Dnn;

#endregion

namespace DlibFaceRecognition
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (HasNoArguments(args)) return;

            var faces = new List<Matrix<RgbPixel>>();

            using var detector = Dlib.GetFrontalFaceDetector();

            using var shapePredictor =
                ShapePredictor.Deserialize(
                    "shape_predictor_5_face_landmarks.dat");
            
            using var metric =
                LossMetric.Deserialize(
                    "dlib_face_recognition_resnet_model_v1.dat");

            DetectFaces(args, faces, detector, shapePredictor);

            if (HasNoFace(faces)) return;
            RecognizeFaces(metric, faces);
        }

        private static bool HasNoArguments(string[] inputArgs)
        {
            if (inputArgs.Length != 0) return false;
            Console.WriteLine(
                "Usage : dotnet run <First_Image_Directory> <Second_Directory>");
            return true;
        }

        private static void DetectFaces(
            IEnumerable<string> imagePaths,
            List<Matrix<RgbPixel>> faces,
            FrontalFaceDetector faceDetector,
            ShapePredictor shapePredictor)
        {
            foreach (var imagePath in imagePaths)
            {
                using var image =
                    Dlib.LoadImageAsMatrix<RgbPixel>(imagePath);
                faces.AddRange(
                    from face in faceDetector.Operator(image)
                    select shapePredictor.Detect(image, face)
                    into shape
                    select Dlib.GetFaceChipDetails(shape, 150, 0.25)
                    into faceChipDetail
                    select Dlib.ExtractImageChip<RgbPixel>
                        (image, faceChipDetail));
            }
        }

        private static void RecognizeFaces(LossMetric metric,
            IEnumerable<Matrix<RgbPixel>> faces)
        {
            var faceDescriptors = metric.Operator(faces);
            var edges = new List<SamplePair>();
            for (uint i = 0; i < faceDescriptors.Count; ++i)
            for (var j = i; j < faceDescriptors.Count; ++j)
            {
                var difference = faceDescriptors[i] - faceDescriptors[j];
                if (Dlib.Length(difference) < 0.6)
                    edges.Add(new SamplePair(i, j));
            }

            Dlib.ChineseWhispers(
                edges, 100,
                out var numClusters,
                out _);
            Console.WriteLine(
                $"number of people found in images: {numClusters}");
        }

        private static bool HasNoFace(ICollection faces)
        {
            if (faces.Count >= 1) return false;
            Console.WriteLine("Pictures Contain No Image!!");
            return true;
        }
    }
}