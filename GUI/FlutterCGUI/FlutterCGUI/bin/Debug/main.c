int main(double n){
	double res = binpow(1.001, (int)n);
}

double binpow(double a, int b){
	if (b == 0) return 1;
	if (b % 2 == 1) return a * binpow(a, b - 1);
	double root = binpow(a, b / 2);
	return root*root;
}