int isdigit(char x){
	if(x>='0' && x<='9'){
		return 1;
	}
	else{
		return 0;
	}
}


int islower(char x){
	if(x>='a' && x<='z'){
		return 1;
	}
	else{
		return 0;
	}
}

int isupper(char x){
	if(x>='A' && x<='Z'){
		return 1;
	}
	else{
		return 0;
	}
}


int isspace(char x){
	if(x==' '){
		return 1;
	}
	else{
		return 0;
	}
}


int isxdigit(char x){
	if((x>='A' && x<='F') || isdigit(x)){
		return 1;
	}
	else{
		return 0;
	}
}


int isalpha(char x){
	if(islower(x) || isupper(x)){
		return 1;
	}
	else{
		return 0;
	}
}

int tolower(char x){
	return 'a'+(x - 'A');
}

int toupper(char x){
	return 'A' + (x - 'a');
}