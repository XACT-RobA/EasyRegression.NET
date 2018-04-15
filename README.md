# EasyRegression.NET

[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT) [![Build Status](https://travis-ci.org/XACT-RobA/EasyRegression.NET.svg?branch=master)](https://travis-ci.org/XACT-RobA/EasyRegression.NET) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/38176ef1f92440199875caf61eb70121)](https://www.codacy.com/app/XACT-RobA/EasyRegression.NET?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=XACT-RobA/EasyRegression.NET&amp;utm_campaign=Badge_Grade)

---

## Aim
- Easy to use regression library for .Net Core applications
- Flexible data preprocessing
- Fast single/multi threaded optimisation
- Reliable data prediction
- Useful progress logging
- Unit test all calculation based methods
- Solid integration tests
- Minimal dependancies (currently just Newtonsoft.Json)

## Usage

EasyRegression requires a jagged double array of input values `x` and a double array of output values `y`.
The `x` values can be nullable doubles, for cases where there is missing data, as these values will be filled during preprocessing.

```cs
// Create an instance of the LinearRegressionEngine class with no parameters
var regression = new LinearRegressionEngine();

// Train the linear regression engine using real data
// x is either double[][] or double?[][]
// y is double[]
// x will be preprocessed before training begins
regressionEngine.Train(x, y);

// Predict y0 value based on input x0 where x0 is double[] or double?[]
// x0 will be preprocessed before the prediction is made
double y0 = regressionEngine.Predict(x0);
```

Serialising a trained regression engine allows the user to re-use the same preprocessing parameters and trained regression model for predictions without further training in the future. The `Serialise` method outputs a json version of the current trained setup as a string, which can then be stored in a file or database for later use.

It is however recommended to train the engine from scratch as new batches of data are added to keep the model as correct and up to data as possible.

```cs
// Create and train a linear regression engine as in the previous example
var regression = new LinearRegressionEngine();
regression.Train(x, y);

// Store the trained regression parameters as json
string json = regressionEngine.Serialise();

// Create a new instance of the LinearRegressionEngine from the serialised json
var newRegressionEngine = LinearRegressionEngine.Deserialise(json);

// Predict y1 value base on input x1
// x1 will be preprocessed the same as x0 was before predicting
double y1 = newRegressionEngine.Predict(x1);
```

By default, the regression engine will include a preprocessor that will fill any invalid data (null, nan, infinite) with the mean of that feature across the dataset. It will then "smooth" the data using standardisation.

To change this functionality, a preprocessor instance can be passed into the LinearRegressionEngine using Set methods.

```cs
// Create a set of preprocessing plugins to configure the preprocessor
IDataPatcher medianPatcher = new MedianDataPatcher();
IDataFilter standardDeviationFilter = new StandardDeviationFilter();
IDataExpander polynomialExpander = new PolynomialDataExpander(order: 2);
IDataSmoother normaliser = new DataNormaliser();

// Create instance of the Preprocessor class
IPreprocessor preprocessor = new Preprocessor();
preprocessor.SetDataPatcher(medianPatcher);
preprocessor.SetDataFilter(standardDeviationFilter);
preprocessor.SetDataExpander(polynomialExpander);
preprocessor.SetDataSmoother(normalise);

// Create instance of LinearRegressionEngine and pass in the custom/configured preprocessor
var regression = new LinearRegressionEngine();
regression.SetPreprocessor(preprocessor);
```

The preprocessor comprises of a data patcher, a data filter, a data expander, and a data smoother.

For the data patcher, the user currently has the choice of patching invalid data with the feature mean, feature median, or zero. If the user doesn't want to use any of these data patchers, and instead wants any data rows containing invalid data to be removed, there will also be a DeletingDataPatcher.

```cs
// Create mean data patcher
IDataPatcher meanPatcher = new MeanDataPatcher();

// Create median data patcher
IDataPatcher medianPatcher = new MedianDataPatcher();

// Create zero data patcher
IDataPatcher zeroPatcher = new ZeroDataPatcher();

// Patch input data where data is either double[][] or double?[][]
double[][] patchedData = meanPatcher.Patch(data);

// Pass a specific data patcher into a Preprocessor
IPreprocessor preprocessor = new Preprocessor();
preprocessor.SetDataPatcher(meanPatcher);
```

The default data expander is InterceptExpander, which currently just adds an intercet column of value 1.0, that doesn't get smoothed like the rest of the data. There is also the PolynominalExpander, which has the effect of raising each variable to every power up to the input order, and then creates products of all of the polynomial powers. This expands data exponentially as columns and order increase. Finally, in cases where the user does not want to add an intercept to their data, there is the BlankDataExpander, though this is not recommended.

```cs
// Create polynomial expander with polynomial order 1
// [x0, x1] => [1, x1, x0, x0x1]
IDataExpander polynomialExpander = new PolynomialDataExpander(order: 1);

// Create intercept expander
// [x0, x1] => [1, x0, x1]
IDataExpander interceptExpander = new InterceptDataExpander();

// Create blank expander
// [x0, x1] => [x0, x1]
IDataExpander blankExpander = new BlankDataExpander();

// Expand a double[][] of data
// This expander creates huge amounts of data as data.Length and order increase
// Creates Math.Pow(order + 1, data.Length) features
double[][] expandedData = polynomialExpander.Expand(data);

// Pass an expander into a preprocessor
IPreprocessor preprocessor = new Preprocessor();
preprocessor.SetDataExpander(polynomialExpander);
```

The user can also currently choose between three data smoothers, one that normalises the data, one that standardises the data, and one that performs no data smoothing. [(link)](http://www.dataminingblog.com/standardization-vs-normalization/)

```cs
// Create data normaliser
// X := (x - min) / (max - min)
IDataSmoother normaliser = new DataNormaliser();

// Create data standardiser
// X := (x - 𝜎) / 𝜇
IDataSmoother standardiser = new DataStandardiser();

// Create blank data smoother
// X := x
IDataSmoother blankSmoother = new BlankDataSmoother();

// Smooth input data where data is double[][]
double[][] smoothedData = standardiser.Smooth(data);

// Pass a specific data smoother into a Preprocessor
IPreprocessor preprocessor = new Preprocessor();
preprocessor.SetDataSmoother(normaliser);
```

## Progress

### Preprocessing

Item | Completed | Tested | Logged
-----|-----------|--------|-------
**Data patching** | | |
Mean patching | ✓ | ✓ | -
Median pathing | ✓ | ✓ | -
Zero patching | ✓ | ✓ | -
**Data smoothing** | | |
Normalisation | ✓ | ✓ | -
Standardisation | ✓ | ✓ | -
**Data expansion** | | |
Polynomial product expansion | ✓ | - | -
Intercept expansion | ✓ | - | -
**Outlier filtering** | | |
IQR multiple from median | ✓ | - | -
Stdev multiple from mean | ✓ | - | -

### Optimisation

Item | Completed | Tested | Logged
-----|-----------|--------|-------
**Linear regression** | | |
Batch gradient descent | ✓ | - | -
Stochastic gradient descent | - | - | -
Mini-batch descent | - | - | -
Multi threaded BGD | - | - | -

### Prediction
Item | Completed | Tested | Logged
-----|-----------|--------|-------
Data prediction | ✓ | ✓ | -
**Reproducible predictions** | | |
Config saving | ✓ | ✓ | -
Config loading | ✓ | ✓ | -

### Integration Testing
Item | Completed
-----|----------
Preprocessing | - | -
Optimisation | - | -
Prediction | - | -