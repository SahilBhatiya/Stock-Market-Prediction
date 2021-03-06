﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.LightGbm;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace StockMarket
{
    public partial class StockPrediction
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"Id", @"Id"),new InputOutputColumnPair(@"Volume", @"Volume")})      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"date", @"date"))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"Id",@"Volume",@"date"}))      
                                    .Append(mlContext.Regression.Trainers.LightGbm(new LightGbmRegressionTrainer.Options(){NumberOfLeaves=200,MinimumExampleCountPerLeaf=2,NumberOfIterations=4,MaximumBinCountPerFeature=639,LearningRate=1.80528781604353E-09F,LabelColumnName=@"ClosePrice",FeatureColumnName=@"Features",Booster=new GradientBooster.Options(){SubsampleFraction=1F,FeatureFraction=1F,L1Regularization=2E-10F,L2Regularization=2E-10F}}));

            return pipeline;
        }
    }
}
