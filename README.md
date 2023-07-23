# Portfolio Optimization
<h3>Model description </h3>
<h4>Notations</h4>
We use the following notations:

$U$ - volume of available funds;

$n$ - total number of financial assets;

$a_i$ - the price of an asset unit $i$;

$x_i^- (x_i^+)$ - the minimum (maximum) number of asset units in the portfolio;

$x_i$ - the optimization variable, the volume of the asset i (here, $x_i\in\[x_i^-,x_i^+\]$)

$y_i$ - the expected income of asset $i$.

<h4>Objective function</h4>
The following objective function is maximized:

$$\dfrac{\sum\limits_{i=1}^{n}a_{i}y_{i}x_{i}}{\sum\limits_{i=1}^{n}a_{i}x_{i}}-\dfrac{2|V-U|-(V-U)}{10U}$$
The profitability of the formed portfolio is:

Here, $\dfrac{\sum\limits_{i=1}^{n}a_{i}y_{i}x_{i}}{\sum\limits_{i=1}^{n}a_{i}x_{i}}$ is the profitability of the obtained portfolio and 
$\dfrac{2|V-U|-(V-U)}{10U}$ is the fine for exceeding the math model.

<h3>Data description</h3>
The input data is stored in .xlsl file
