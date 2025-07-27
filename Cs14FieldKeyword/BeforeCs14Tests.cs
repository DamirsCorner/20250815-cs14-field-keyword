using Shouldly;

namespace Cs14FieldKeyword;

public class BeforeCs14Tests
{
    public class SampleClass
    {
        public int NonValidatedProperty { get; set; }

        private int validatedPropertyValue;
        public int ValidatedProperty
        {
            get { return validatedPropertyValue; }
            set
            {
                validatedPropertyValue =
                    value >= 0
                        ? value
                        : throw new ArgumentOutOfRangeException(
                            nameof(value),
                            "The value must be non-negative."
                        );
            }
        }

        public void SetBackingField(int value)
        {
            validatedPropertyValue = value;
        }
    }

    [Test]
    public void SettingNonValidatedPropertySucceeds()
    {
        SampleClass instance = new();

        instance.NonValidatedProperty = -1;

        instance.NonValidatedProperty.ShouldBe(-1);
    }

    [Test]
    public void SettingValidatedPropertyThrows()
    {
        SampleClass instance = new();

        Should.Throw<ArgumentOutOfRangeException>(() => instance.ValidatedProperty = -1);

        instance.ValidatedProperty.ShouldBe(0);
    }

    [Test]
    public void SettingValidatedPropertyWithoutValidationSucceeds()
    {
        SampleClass instance = new();

        instance.SetBackingField(-1);

        instance.ValidatedProperty.ShouldBe(-1);
    }
}
