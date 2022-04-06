export interface UserData {
  id: string;
  userName: string;
  email: string;
  type: string;
}

export interface LoginUserForm {
  login: string;
  password: string;
}

export interface RegisterDoctorForm {
  firstName: string;
  lastName: string;
  dateOfBurth: Date;
  zipCode: string;
  country: string;
  city: string;
  specializationId: string;
  licenseNo: string;
  workStart: Date;
  workAddress: string;
  userName: string;
  email: string;
  phoneNumber: string;
  type: string;
  password: string;
  confirmPassword: string;
}

export interface RegisterPatientForm {
  firstName: string;
  lastName: string;
  dateOfBurth: Date;
  zipCode: string;
  country: string;
  city: string;
  userName: string;
  email: string;
  phoneNumber: string;
  type: string;
  password: string;
  confirmPassword: string;
}

export interface AuthResult {
  success: boolean;
  errors?: string[];
  user?: UserData;
  token?: string;
}

const getHeaders = (): Headers => {
  let headers = new Headers();

  headers.append("Content-Type", "application/json");
  headers.append("Accept", "application/json");
  headers.append("Access-Control-Allow-Origin", "http://localhost:5000");
  headers.append("Access-Control-Allow-Headers", "true");
  headers.append("Origin", "http://localhost:5000");

  return headers;
};

const addUserToAuthResult = async (
  userName: string,
  token: string,
): Promise<AuthResult> => {
  let headers = getHeaders();

  headers.append("Authorization", "bearer " + token);

  const userResponse = await fetch(
    "http://localhost:5000/userByLogin?userLogin=" + userName,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  let userResult = await userResponse.json();

  if (userResult.errors !== undefined)
    return { success: false, errors: userResult.errors };

  return { success: true, token: token, user: userResult };
};

export const loginUser = async (user: LoginUserForm): Promise<AuthResult> => {
  let headers = getHeaders();

  const response = await fetch("http://localhost:5000/login", {
    mode: "cors",
    method: "POST",
    body: JSON.stringify(user),
    headers: headers,
  });

  let result = await response.json();

  if (result.errors !== undefined)
    return { success: false, errors: result.errors };

  return await addUserToAuthResult(user.login, result.token);
};

export const registerDoctor = async (
  doctor: RegisterDoctorForm,
): Promise<AuthResult> => {
  doctor.type = "Doctor";

  let headers = getHeaders();

  const response = await fetch("http://localhost:5000/registerDoctor", {
    mode: "cors",
    method: "POST",
    body: JSON.stringify(doctor),
    headers: headers,
  });

  let result = await response.json();

  if (result.errors !== undefined)
    return { success: false, errors: result.errors };

  return await addUserToAuthResult(doctor.userName, result.token);
};

export const registerPatient = async (
  patient: RegisterPatientForm,
): Promise<AuthResult> => {
  patient.type = "Patient";

  let headers = getHeaders();

  const response = await fetch("http://localhost:5000/registerPatient", {
    mode: "cors",
    method: "POST",
    body: JSON.stringify(patient),
    headers: headers,
  });

  let result = await response.json();

  if (result.errors !== undefined)
    return { success: false, errors: result.errors };

  return await addUserToAuthResult(patient.userName, result.token);
};
