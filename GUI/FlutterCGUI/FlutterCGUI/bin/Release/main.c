int main(double n){
	/*
	printf("|-0.5| %f %f\n", fabs(-0.5), 0.5);
	printf("sin(0.4) %f %f\n", sin(0.4), 0.389418);
	printf("cos(0.4) %f %f\n", cos(0.4), 0.921061);
	printf("tan(0.4) %f %f\n", tan(0.4), 0.422793);
	printf("asin(0.4) %f %f\n", asin(0.4), 0.411517);
	printf("acos(0.4) %f %f\n", acos(0.4), 1.159279);
	printf("atan(0.4) %f %f\n", atan(0.4), 0.380506);
	printf("pow(2.3, 4.7) %f %f\n", pow(2.3, 4.7), 50.132669);
	printf("ldexp(2.3, 5) %f %f\n", ldexp(2.3, 5), 73.6);
	printf("fmod(2.3, 0.4) %f %f\n", fmod(2.3, 0.4), 0.3);
 
	int res1 = 0;
	double res2 = 0;
	printf("frexp(152.85) %f %d %f %d\n", frexp(152.85, &res1), res1, 0.59707, 8);
	printf("modf(152.85) %f %f %f %f\n", modf(152.85, &res2), res2, 152, 0.85);

	printf("floor(1.45) %f %f\n", floor(1.45), 1);
	printf("floor(-1.45) %f %f\n", floor(-1.45), -2);
	printf("ceil(1.45) %f %f\n", ceil(1.45), 2);
	printf("ceil(-1.45) %f %f\n", ceil(-1.45), -1);
	*/


	/*
	double a;
	double b;
	scanf("%f %f", &a, &b);
	double res = pow(a, b);
	printf("%f\n", res);
	*/
	
	
	int len = (int)n;
	int* array = makeArray(len);
	printArray(array, len);
	bubbleSort(array, len);
	printArray(array, len);
	
	
	/*
	double a = binpow(1.001, (int)n);
	printf("%f\n", a);
	*/
}

int* makeArray(int len){
	int* res = (int*)malloc(sizeof(int)*len);
	for (int i = 0; i<len; i++){
		res[i] = rand() % 10;
	}
	return res;
}

void bubbleSort(int* a, int len){
	for (int i = 0; i<len; i++){
		for (int j = 0; j<len-i-1; j++){
			if (a[j] > a[j + 1]){
				int temp = a[j];
				a[j] = a[j + 1];
				a[j + 1] = temp;
			}
		}
	}
	//dump();
}

void printArray(int* a, int len){
	for (int i = 0; i<len; i++){
		printf("%d ", a[i]);
	}
	printf("\n");
}












































/*
	char* a1 = (char*)malloc(5*sizeof(char));
	char* b1 = "abc";
	printf("%s %s\n", a1, strcpy(a1, b1), "abc");
	
	char* a2 = (char*)malloc(5*sizeof(char));
	char* b2 = "abc";
	printf("%s %s\n", strncpy(a2, b2, 2), "ab");
	
	char* a3 = (char*)malloc(10*sizeof(char));
	a3[0] = 'a';
	a3[1] = 'b';
	char* b3 = "abc";
	printf("%s %s\n", strcat(a3, b3), "ababc");
	
	char* a4 = "abc";
	char* b4 = "aba";
	printf("%d %d\n", strcmp(a4, b4), 1);
	
	char* a5 = "abc";
	char* b5 = "abcd";
	printf("%d %d\n", strcmp(a5, b5), -1);
	
	char* a6 = "abc";
	char* b6 = "aba";
	printf("%d %d\n", strncmp(a6, b6, 3), 1);
	
	char* a7 = "abc";
	char* b7 = "abcd";
	printf("%d %d\n", strncmp(a7, b7, 3), 0);
	
	char* a8 = "abc";
	char* b8 = "abcd";
	printf("%d %d\n", strncmp(a8, b8, 3), 0);
	
	char* a9 = "abcabc";
	printf("%s %s\n", strchr(a9, 'b'), "bcabc");
	
	char* a10 = "abcabc";
	printf("%s %s\n", strrchr(a10, 'b'), "bc");
	
	char* a11 = "cdbc";
	char* b11 = "cd";
	printf("%d %d\n", strspn(a11, b11), 2);
	
	char* a12 = "trcdbc";
	char* b12 = "tc";
	printf("%d %d\n", strcspn(a12, b12), 1);
	
	char* a13 = "trcdbc";
	char* b13 = "tc";
	printf("%d %d\n", strcspn(a13, b13), 1);
	
	char* a14 = "trcdbc";
	char* b14 = "tc";
	printf("%d %d\n", strcspn(a14, b14), 1);
	
	char* a15 = "trcdbc";
	char* b15 = "ce";
	printf("%s %s\n", strrbrk(a15, b15), "cdbc");

	
	char* a16 = "trcdbc";
	char* b16 = "cdb";
	printf("%s %s\n", strstr(a16, b16), "cdbc");
	
	
	char* src="123456";
    char* dst=(char*)malloc(10*sizeof(char));
    memcpy(dst, src, 6);
    printf("%s\n",dst);

	char* src2="123456";
    char* dst2=(char*)malloc(10*sizeof(char));
    memmove(dst2, src2, 4);
    printf("%s\n",dst2);


	char* src3="123456";
    memset(src3, 'u', 4);
    printf("%s\n", src3);
    
    
    
    
    
    
    
    
    
	printf("%d\n", isdigit('5'));
	printf("%d\n", isdigit('a'));
	printf("%d\n", isalpha('A'));
	printf("%d\n", isalpha('b'));
	printf("%d\n", isalpha('$'));
	printf("%d\n", isalpha('4'));
	printf("%d\n", isxdigit('A'));
	printf("%d\n", isxdigit('X'));
	printf("%d\n", isxdigit('1'));
	printf("%c\n", tolower('X'));
	printf("%c\n", toupper('t'));




	long a = atol("23434312323443");
	printf("%ld\n", a);

	long b = atoi("4578");
	printf("%d\n", b);

	double c = atof("36.9");
	printf("%f\n", c);
*/