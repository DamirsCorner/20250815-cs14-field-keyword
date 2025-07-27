using Shouldly;

namespace Cs14FieldKeyword;

public class WithCs14Tests
{
    public class SampleClass
    {
        public int ValidatedProperty
        {
            get;
            set
            {
                field =
                    value >= 0
                        ? value
                        : throw new ArgumentOutOfRangeException(
                            nameof(value),
                            "The value must be non-negative."
                        );
            }
        }

        private int field;
        public int FieldProperty
        {
            get { return @field; }
            set { this.field = value; }
        }

        public int GetFieldValue()
        {
            return field;
        }
    }

    [Test]
    public void SettingValidatedPropertyThrows()
    {
        SampleClass instance = new();

        Should.Throw<ArgumentOutOfRangeException>(() => instance.ValidatedProperty = -1);

        instance.ValidatedProperty.ShouldBe(0);
    }

    [Test]
    public void SettingValidatedPropertyDoesNotSetDeclaredField()
    {
        SampleClass instance = new();

        instance.ValidatedProperty = 1;

        instance.GetFieldValue().ShouldBe(0);
    }

    [Test]
    public void SettingFieldPropertySetsDeclaredField()
    {
        SampleClass instance = new();

        instance.FieldProperty = 1;

        instance.GetFieldValue().ShouldBe(1);
    }
}
