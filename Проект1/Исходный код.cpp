#include <math.h>

//void main(){
//
class IMath
{
public:
   virtual long Add(long Op1, long Op2) = 0;
   virtual long Subtract(long Op1, long Op2) = 0;
   virtual long Multiply(long Op1, long Op2) = 0;
   virtual long Divide (long Op1, long Op2) = 0;
};

class Math:public IMath
{	
public:
   long Add(long Op1, long Op2);
   long Subtract(long Op1, long Op2);
   long Multiply(long Op1, long Op2);
   long Divide (long Op1, long Op2);
};
//};
