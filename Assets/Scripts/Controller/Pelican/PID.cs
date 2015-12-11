[System.Serializable]
public class PID {

    public float Output;

    public float Kp, Ki, Kd;
	private float integral;
	private float lastError;

    public float P, I, D;

    public float LastError
    {
        get
        {
            return lastError;
        }
    }

	public PID(float pFactor, float iFactor, float dFactor) {
		this.Kp = pFactor;
		this.Ki = iFactor;
		this.Kd = dFactor;
	}

	public float Update(float setpoint, float actual, float timeFrame) {
		float error = setpoint - actual;
        P = error;
        integral += error * timeFrame;
        I = integral;
        float deriv = (error - lastError) / timeFrame;
        D = deriv;
        lastError = error;
        Output = error * Kp + integral * Ki + deriv * Kd;
        return Output;
	}

    public float Update(float error, float timeFrame)
    {
        P = error;
        integral += error * timeFrame;
        I = integral;
        float deriv = (error - lastError) / timeFrame;
        D = deriv;
        lastError = error;
        Output = error * Kp + integral * Ki + deriv * Kd;
        return Output;
    }
}
