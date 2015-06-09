
    public class FieldName : Value
    {
        public string fieldName;


        public FieldName(string fieldName)
        {
            this.fieldName = fieldName;
        }

        public override string ToString()
        {
            return "get field = "+this.fieldName;
        }
    }

