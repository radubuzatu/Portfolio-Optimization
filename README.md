# Portfolio Optimization
<h3>Model description </h3>
<h4>Notations</h4>
We use the following notations:

$U$ - volume of available funds;

$n$ - total number of financial assets;

$a_i$ - the price of an asset unit $i$;

$x_i^- (x_i^+)$ - the minimum (maximum) number of an asset $i$ in the portfolio;

$x_i$ - the optimization variable, the volume of an asset $i$, $x_i\in\[x_i^-,x_i^+\]$

$y_i$ - the expected income of an asset $i$.

<h4>Objective function</h4>
The following objective function is maximized:

$$\dfrac{\sum\limits_{i=1}^{n}a_{i}y_{i}x_{i}}{\sum\limits_{i=1}^{n}a_{i}x_{i}}-\dfrac{2|V-U|-(V-U)}{10U},$$

where $\dfrac{\sum\limits_{i=1}^{n}a_{i}y_{i}x_{i}}{\sum\limits_{i=1}^{n}a_{i}x_{i}}$ is the profitability of the obtained portfolio, 
$\dfrac{2|V-U|-(V-U)}{10U}$ is the fine for exceeding the math model, and $V$ is the spent capital.

<h4>Solving approach</h4>

A Genetic Algorithm is used to determine a good solution of Portfolio Optimization Problem.

<h3>Data description</h3>

The input data is stored in .xlsx file as follows:

<img align="center" width="65%" height="65%" src="https://github.com/radubuzatu/Portfolio-Optimization/blob/main/img/data.png">

The input data can be easily imported into the program by using the program menu.

<h3>An example of the program</h3>

After starting the program

<img align="center" width="75%" height="75%" src="https://github.com/radubuzatu/Portfolio-Optimization/blob/main/img/portfolio_start.png">

After importing the data

<img align="center" width="75%" height="75%" src="https://github.com/radubuzatu/Portfolio-Optimization/blob/main/img/portfolio_with_data.png">

After executing the Genetic Algorithm

<img align="center" width="75%" height="75%" src="https://github.com/radubuzatu/Portfolio-Optimization/blob/main/img/portfolio_optimized.png">
