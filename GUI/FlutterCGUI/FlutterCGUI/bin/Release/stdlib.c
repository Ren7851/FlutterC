long strtol(char* nPtr, char **endPtr, int base)
{
	char* start;
	int number;
	long int sum = 0;
	int sign = 1;
	char* pos = nPtr;

	if (*pos == '\0'){
		return 0;
	}

	start = pos;
	while (isspace(*pos))
	{
		++pos;
	}

	if (*pos == '-')
	{
		sign = -1;
		++pos;
	}

	if (*pos == '+')
		++pos;

	if (base == 16 || base == 8)
	{
		if (base == 16 && *pos == '0')
			++pos;
		if (base == 16 && (*pos == 'x' || *pos == 'X'))
			++pos;
		if (base == 8 && *pos == '0')
			++pos;
	}

	if (base == 0)
	{
		base = 10;
		if (*pos == '0')
		{
			base = 8;
			++pos;
			if (*pos == 'x' || *pos == 'X')
			{
				base = 16;
				++pos;
			}
		}
	}

	while (*pos != '\0')
	{
		number = -1;
		if ((int)(*pos) >= 48 && (int)(*pos) <= 57)
		{
			number = (int)(*pos) - 48;
		}
		if (isalpha(*pos))
		{
			number = (int)(toupper(*pos)) - 55;
		}

		if (number < base && number != -1)
		{
			if (sign == -1)
			{
				sum = sum * base - number;
			}
			else
			{
				sum = sum * base + number;
			}
		}
		else if (base == 16 && number > base
			&& (*(pos - 1) == 'x' || *(pos - 1) == 'X'))
		{
			--pos;
			break;
		}
		else
			break;
		++pos;
	}

	if (!isdigit(*(pos - 1)) && !isalpha(*(pos - 1)))
		pos = start;

	if (endPtr)
		*endPtr = (char*)pos;

	return sum;
}



double strtod(char* str, char** endptr){
    double result = 0.0;
    char signedResult = '\0';
    char signedExponent = '\0';
    int decimals = 0;
    int isExponent = 0;
    int hasExponent = 0;
    int hasResult = 0;
	
    double exponent = 0;
    char c;
    for (; '\0' != (c = *str); ++str)
    {
        if ((c >= '0') && (c <= '9'))
        {
            int digit = c - '0';
            if (isExponent)
            {
                exponent = (10 * exponent) + digit;
                hasExponent = 1;
            }
            else 
		if (decimals == 0)
		{
			result = (10 * result) + digit;
			hasResult = 1;
		}
		else
		{
			result += (double)digit / decimals;
			decimals *= 10;
		}
            continue;
        }
        if (c == '.')
        {
            if (!hasResult)
            {
                break;
            }
            if (isExponent)
            {
                break;
            }
            if (decimals != 0)
            {
                break;
            }
            decimals = 10;
            continue;
        }
        if ((c == '-') || (c == '+'))
        {
            if (isExponent)
            {
                if (signedExponent || (exponent != 0))
                    break;
                else
                    signedExponent = c;
				
            }
            else
            {
                if (signedResult || (result != 0))
                    break;
                else
                    signedResult = c;
            }
            continue;
        }
        if (c == 'E')
        {
            if (!hasResult)
            {
                break;
            }
            if (isExponent)
                break;
            else
                isExponent = 1;
            continue;
        }
        break;
    }
    if (isExponent && !hasExponent)
    {
        while (*str != 'E')
            --str;
    }
    if (!hasResult && signedResult)
        --str;

    if (endptr)
        *endptr = (char*)(str);
		
    for (; exponent != 0; --exponent)
    {
        if (signedExponent == '-')
            result /= 10;
        else
            result *= 10;
    }
    if (signedResult == '-')
    {
        if (result != 0)
            result = -result;
    }
    return result;
}



double atof(char* s){
	return strtod(s, (char**)0);
}

int atoi(char* s){
	return (int)strtol(s, (char**)0, 10);
}

long atol(char* s){
	return strtol(s, (char**)0, 10);
}
