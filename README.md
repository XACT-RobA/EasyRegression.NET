# EasyRegression.NET

[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)

---

## Aim
- Easy to use regression library for .Net Core applications
- Flexible data preprocessing
- Fast single/multi threaded optimisation
- Reliable data prediction
- Useful progress logging
- Unit test all calculation based methods
- Solid integration tests

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

By default, the regression engine will include a preprocessor that will fill any invalid data (null, nan, infinite) with the mean of that feature across the dataset. It will then "smooth" the data using standardisation.

To change this functionality, a preprocessor instance can be passed into the constructor of the LinearRegressionEngine.

```cs
// Create a set of preprocessing plugins to configure the preprocessor
IDataPatcher medianPatcher = new MedianDataPatcher();
IDataSmoother normaliser = new DataNormaliser();
IDataExpander polynomialExpander = new PolynomialDataExpander(order: 2);

// Create instance of the Preprocessor class
IPreprocessor pre = new Preprocessor(dataPatcher: medianPatcher,
    dataSmoother: normaliser,
    dataExpander: polynomialExpander);

// Create instance of LinearRegressionEngine passing in the custom/configured preprocessor
var regression = new LinearRegressionEngine(preprocessor: pre);
```

The preprocessor comprises of a data patcher, a data smoother, a data expander, and a data filter.

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
IPreprocessor preprocessor = new Preprocessor(dataPatcher: medianPatcher);
```

The user can also currently choose between two data smoothers, one that normalises the data, and one that standardises the data. [(link)](http://www.dataminingblog.com/standardization-vs-normalization/)
In the case that the user doesn't want the data to be preprocessed in this manner, there is also a BlankDataSmoother that leaves all data as it is, though this isn't recommended.

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
IPreprocessor preprocessor = new Preprocessor(dataSmoother: normaliser);
```

The only non-blank data expander currently implemented is the PolynomialExpander, which expands the number of columns in a dataset to a polynomial power, using each input as a variable to be expanded.
This is not enabled by default, as it's use cases are quite specific, and it has the effect of expanding exponentially large with more columns and higher polynomial orders.

```cs
// Create polynomial expander with polynomial order 1
// [x0, x1] => [1, x1, x0, x0x1]
IDataExpander polynomialExpander = new PolynomialDataExpander(order: 1);

// Expand a double[][] of data
// This expander creates huge amounts of data as data.Length and order increase
// Creates Math.Pow(order + 1, data.Length) features
double[][] expandedData = polynomialExpander.Expand(data);

// Pass an expander into a preprocessor
IPreprocessor preprocessor = new Preprocessor(dataExpander: polynomialExpander);
```

## Progress

### Preprocessing

Item | Completed | Tested
-----|-----------|-------
**Data patching** | |
Mean patching | ✓ | -
Median pathing | ✓ | -
Zero patching | ✓ | -
**Data smoothing** | |
Normalisation | ✓ | -
Standardisation | ✓ | -
**Data expansion** | |
Polynomial expansion | ✓ | -
**Outlier filtering** | |
IQR multiple from median | - | -
Stdev multiple from mean | - | -

### Optimisation

Item | Completed | Tested
-----|-----------|-------
**Linear regression** | |
Batch gradient descent | ✓ | -
Stochastci gradient descent | - | -
Mini-batch descent | - | -
Multi threaded BGD | - | -

### Prediction
Item | Completed | Tested
-----|-----------|-------
Data prediction | - | -

### Integration Testing
Item | Completed
-----|----------
Preprocessing | - | -
Optimisation | - | -
Prediction | - | -