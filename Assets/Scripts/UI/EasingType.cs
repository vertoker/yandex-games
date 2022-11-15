namespace Scripts.UI
{
    // ���� ������� ���������
    public enum EasingType
    {
        Linear = 0, Constant = 1,// ��� ���������
        InSine = 2, OutSine = 3, InOutSine = 4,// ��������� �������
        InQuad = 5, OutQuad = 6, InOutQuad = 7,// 2-��������� ������� (�������)
        InCubic = 8, OutCubic = 9, InOutCubic = 10,// 3-��������� ������� (���)
        InQuart = 11, OutQuart = 12, InOutQuart = 13,// 4-��������� ������� (���������)
        InQuint = 14, OutQuint = 15, InOutQuint = 16,// 5-��������� �������
        InExpo = 17, OutExpo = 18, InOutExpo = 19,// ���������������� �������
        InCirc = 20, OutCirc = 21, InOutCirc = 22,// �������� �������
        InBack = 23, OutBack = 24, InOutBack = 25,// ������������ �������
        InElastic = 26, OutElastic = 27, InOutElastic = 28// ���������� �������
    }
}