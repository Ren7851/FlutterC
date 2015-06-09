char* strcpy(char* strDest, char* strSrc){
	char* temp = strDest;
    while(*strDest++ = *strSrc++);
    return temp;
}


char* strncpy(char* s1, char* s2, int n){
	char *s = s1;
    while (n > 0 && *s2 != '\0') {
		*s++ = *s2++;
		--n;
    }
    while (n > 0) {
		*s++ = '\0';
		--n;
    }
    return s1;
}


char* strcat(char *dest, char *src)
{
    int i = 0;
	int j = 0;
    for (i = 0; dest[i] != '\0'; i++);
    for (j = 0; src[j] != '\0'; j++)
        dest[i+j] = src[j];
    dest[i+j] = '\0';
    return dest;
}


int strcmp(char* s1, char* s2){
	while(*s1 && (*s1==*s2)){
        s1++;
		s2++;
	}
	if(*s1 < *s2){
		return -1;
	}
	else{
		return *s1 > *s2;
	}
}


int strncmp(char* s1, char* s2, int n){
	int c = 1;
	while(*s1 && (*s1==*s2)){
		if(c >= n){
			break;
		}
		
        s1++;
		s2++;
		c++;
	}
	if(*s1 < *s2){
		return -1;
	}
	else{
		return *s1 > *s2;
	}
}


char* strchr(char* s, int c){
	while(1){
		if(*s == c){
			return s;
		}
		if(*s == '\0'){
			return 0;
		}
		s++;
	}
}

char* strrchr(char* s, int c){
	char* newpointer = s+strlen(s);
	while(1){
		if(*newpointer == c){
			return newpointer;
		}
		if(newpointer == s){
			return 0;
		}
		newpointer--;
	}
}



int strlen(char* cs){
	int res = 0;
	while(*cs!='\0'){
		cs++;
		res++;
	}
	return res;
}



int strspn(char* str1, char* str2){
	int i = 0;
	int k = 0;
	int counter = 0;
	for(i=0; str1[i]!='\0'; i++){
		if(counter != i) 
		{
			break;
		}

		for(k=0;str2[k]!='\0';k++){
			if(str1[i]==str2[k]){
				counter++;
			}
		}
	}
	return counter;
}

int strcspn(char* str1, char* str2){
	int i = 0;
	int k = 0;
	int pointer = 0;
	int counter = 0;
	for(i=0; str1[i]!='\0'; i++){
		if(counter != i) break;
		int inc = 1;
		for(k=0;str2[k]!='\0';k++){
			if(str1[i]==str2[k]){
				inc = 0;
			}
		}
		if(!inc){
			counter++;
		}
	}
	return counter;
}

char* strrbrk(char* s1, char* s2){
	char *c = s2;
	if (!*s1) {
		return 0;
	}
	while (*s1) {
		for (c = s2; *c; c++) {
			if (*s1 == *c)
				break;
		}
		if (*c)
			break;
		s1++;
	}
	if (*c == '\0')
		s1 = 0;
	return s1;
}

char* strstr(char* str, char* target){
	if (!*target) return str;
	char *p1 = (char*)str;
	while (*p1) {
		char* p1Begin = p1;
		char* p2 = (char*)target;
		while (*p1 == *p2) {
			p1++;
			p2++;
			if(*p1 == '\0' || *p2 == '\0'){
				break;
			}
		}
		if (!*p2)
			return p1Begin;
		p1 = p1Begin + 1;
	}
	return 0;
}

void* memmove(void* dest, void* src, int n){
	char *d = (char *)dest;
	char *s = (char *)src;

	if (s == d)
		return dest;

	if (s < d) {
		s = s + n - 1;
		d = d + n - 1;
		while (n--)
		{
			*d-- = *s--;
		}
	}
	else {
		while (n--)
			*d++ = *s++;
	}
	return dest;
}

int memcmp(void* s1, void* s2, int n){
	return strncmp(s1, s1, n);
}


void* memset(void* b, char c, int len){
	int i;
	char* p = b;
	i = 0;
	while (len > 0)
	{
		*p = c;
		p++;
		len--;
	}
	return b;
}

void* memcpy(void* dest, void* src, int count){
	char* dst8 = (char*)dest;
	char* src8 = (char*)src;
	while (count--) {
		*dst8 = *src8;
		dst8++;
		src8++;
	}
	return dest;
}
