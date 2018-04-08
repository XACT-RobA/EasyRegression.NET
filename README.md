# EasyRegression.NET

## Aim
- Easy to use regression library for .Net Core applications
- Flexible data preprocessing
- Fast single/multi threaded optimisation
- Reliable data prediction
- Useful progress logging
- Unit test all calculation based methods
- Solid integration tests

## Usage

EasyRegression requires a jagged double array of input values $`x`$ and a double array of output values $`y`$.
The $`x`$ values can be nullable doubles, for cases where there is missing data, as these values will be filled during preprocessing.

```cs
var regression = new LinearRegressionEngine();
regressionEngine.Train(x, y);
var y0 = regressionEngine.Predict(x0);
```

By default, the regression engine will include a preprocessor that will fill any invalid data (null, nan, infinite) with the mean of that feature across the dataset. It will then "smooth" the data using standardisation.

To change this functionality, a preprocessor instance can be passed into the constructor of the LinearRegressionEngine.

```cs
var pre = new Preprocessor();
var regression = new LinearRegressionEngine(preprocessor: pre);
```

The preprocessor comprises of a data patcher, a data smoother, a data expander, and a data filter.

For the data patcher, the user currently has the choice of patching invalid data with the feature mean, feature median, or zero. If the user doesn't want to use any of these data patchers, and instead wants any data rows containing invalid data to be removed, there will also be a DeletingDataPatcher.

```cs
var preprocessor1 = new Preprocessor(dataPatcher: new MeanDataPatcher());

var preprocessor2 = new Preprocessor(dataPatcher: new MedianDataPatcher());

var preprocessor3 = new Preprocessor(dataPatcher: new ZeroDataPatcher());
```

The user can also currently choose between two data smoothers, one that normalises the data, and one that standardises the data. [(link)](http://www.dataminingblog.com/standardization-vs-normalization/)
In the case that the user doesn't want the data to be preprocessed in this manner, there is also a BlankDataSmoother that leaves all data as it is, though this isn't recommended.

```cs

```

The only non-blank data expander currently implemented is the PolynomialExpander, which expands the number of columns in a dataset to a polynomial power, using each input as a variable to be expanded.
This is not enabled by default, as it's use cases are quite specific, and it has the effect of expanding exponentially large with more columns and higher polynomial orders.

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
Batch gradient descent | - | -
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