int readPin = 2;
bool inputData = false;
bool lastInput = false;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(readPin, INPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  inputData = digitalRead(readPin);

  if (inputData == lastInput) {
    return;
  }

  lastInput = inputData;

  if (inputData == true) {
    Serial.println("h");
  } else {
    Serial.println("l");
  }
}
