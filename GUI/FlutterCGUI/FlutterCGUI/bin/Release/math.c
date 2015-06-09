int EDOM = 1;
int errno = 0;
double PI = 3.14159265358;
double EPS = 0.000000001;


double fabs(double x){
	if (x >= 0){
		return x;
	}
	else{
		return -x;
	}
}

double binpow(double a, int b){
	if (b == 0){
		return 1.0;
	}
	else{
		if (b % 2 == 0){
			double part = binpow(a, b / 2);
			return part*part;
		}
		else{
			return a*binpow(a, b - 1);
		}
	}
}

double sin(double x){
	double res = 0;
	int n;
	double member = 0;
	for (n = 0;;n++){
		if (n == 0 || n == 1){
			if (n == 0){
				member = x;
			}
			else{
				member = -x*x*x / 6.0;
			}
		}
		else{
			double i = (double)(n - 1);
			member = -member * x*x * (1.0) / ((2.0*i + 2.0)*(2.0*i + 3.0));
		}
		res += member;
		if (fabs(member) < EPS){
			break;
		}
	}
	return res;
}

double cos(double x){
	double res = 0;
	int n;
	double member = 0;
	for (n = 0;; n++){
		if (n == 0 || n == 1){
			if (n == 0){
				member = 1;
			}
			else{
				member = -x*x / 2.0;
			}
		}
		else{
			double i = (double)(n - 1);
			member = -member * x*x * (1.0) / ((2.0*i + 1.0)*(2.0*i + 2.0));
		}
		res += member;
		if (fabs(member) < EPS){
			break;
		}
	}
	return res;
}


double tan(double x){
	return sin(x) / cos(x);
}

double sqrt(double n){
	double x = 1;
	for (;;) {
		double nx = (x + n / x) / 2.0;
		if (fabs(x - nx) < EPS)  break;
		x = nx;
	}
	return x;
}


double asin(double x){
	double res = 0;
	int n = 0;
	double member = 0;
	double k = 1;
	for (n = 0;; n++){
		if (n == 0){
			member = x;
		}
		else{
			if (n == 1){
				member = x*x*x / 6.0;
			}
			else{
				double e = (double)(n - 1);
				k = (2.0*e + 1.0)*(2.0*e + 1.0)*(2.0*e + 2.0) / (4.0*(e + 1.0)*(e + 1.0)*(2.0*e + 3.0));
				member = member * k*x*x;
			}
		}
		res += member;
		if (fabs(member) < EPS || n>100){
			break;
		}
	}
	return res;
}


double acos(double x){
	return PI / 2.0 - asin(x);
}


double atan(double y){
	double l = -PI / 2.0;
	double r = PI / 2.0;
	double m = (l + r) / 2.0;
	while (fabs(r - l) > EPS){
		m = (l + r) / 2.0;
		double f = tan(m);
		if (f >= y){
			r = m;
		}
		else{
			l = m;
		}
	}
	return m;
}


double exp(double x){
	double res = 0;
	int n = 0;
	double member = 1;
	while (1){
		if (n != 0){
			member = member * x / (double)n;
		}
		res += member;

		if (fabs(member) < EPS){
			return res;
		}

		n++;
	}
	return res;
}


double sinh(double x){
	return (exp(x) - exp(-x)) / 2.0;
}

double cosh(double x){
	return (exp(x) + exp(-x)) / 2.0;
}

double tanh(double x){
	return sinh(x) / cosh(x);
}

double log(double y){
	if (y <= 0){
		errno = EDOM;
		return -100;
	}

	double l = -100.0;
	double r = 100.0;
	double m = (l + r) / 2.0;
	while (fabs(r - l) > EPS){
		m = (l + r) / 2.0;
		double f = exp(m);
		if (f >= y){
			r = m;
		}
		else{
			l = m;
		}
	}
	return m;
}


double log10(double x){
	return log(x) / log(10.0);
}

double pow(double a, double x){
	if (a == 0.0 && x<0){
		errno = EDOM;
		return 0;
	}

	if (a < 0){
		errno = EDOM;
		return 0;
	}

	return exp(x*log(a));
}

double ldexp(double x, int n){
	return x*binpow(2.0, n);
}

double frexp(double x, int* exponent){
	if (x == 0){
		*exponent = 0;
		return 0;
	}

	double sign = 1.0;
	if (x < 0){
		sign = -sign;
		x *= -1.0;
	}

	int ex = 0;

	while (!(x >= 0.5 && x<1.0)){
		if (x>1.0){
			ex++;
			x /= 2.0;
		}
		else{
			ex--;
			x *= 2.0;
		}
	}

	*exponent = ex;
	return x*sign;
}



double ceil(double x){
	if (x >= 0){
		if ((double)((int)x) == x){
			return (double)((int)x);
		}
		else{
			return (double)((int)x) + 1.0;
		}
	}
	else{
		if ((double)((int)x) == x){
			return (double)((int)x);
		}
		else{
			return (double)((int)x);
		}
	}
}

double floor(double x){
	if (x >= 0){
		if ((double)((int)x) == x){
			return (double)((int)x);
		}
		else{
			return (double)((int)x);
		}
	}
	else{
		if ((double)((int)x) == x){
			return (double)((int)x);
		}
		else{
			return (double)((int)x) - 1.0;
		}
	}
}


double modf(double x, double* ip){
	if (x >= 0){
		*ip = x - floor(x);
		return floor(x);
	}
	else{
		*ip = x - ceil(x);
		return ceil(x);
	}
}

double fmod(double x, double y){
	if (y == 0){
		errno = EDOM;
		return -100;
	}

	double div = x / y;
	double res = 0;
	modf(div, &res);
	return res*y;
}
