#include <string>
using namespace std;

class Math
{
// �������� ���������� 
public:
   long Add( long Op1, long Op2 );
   long Subtract( long Qp1, long Op2 );
   long Multiply( long Op1, long ��2 );
   long Divide( long Op1, long Op2 );

private:
   // ����������
	string m_strVersion;
    string get_Version( );
};

long Math::Add( long Op1, long Op2 )
{
   return Op1 + Op2;
}

long Math::Subtract( long Op1, long Op2 )
{
   return Op1 - Op2;
}
long Math::Multiply( long Op1, long Op2 ) {
   return Op1 * Op2;
}
long Math::Divide( long Op1, long Op2 )
{
   return Op1 / Op2;
}