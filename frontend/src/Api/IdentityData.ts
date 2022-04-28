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
  averageAppointmentTime: number;
  workDayStart: Date;
  workDayEnd: Date;
  dinnerStart: Date;
  dinnerEnd: Date;
  doctorsScedule: CreateDoctorSceduleDataForm[];
  doctorDayOffs: CreateDoctorDayOffData[];
  userName: string;
  email: string;
  phoneNumber: string;
  type: string;
  password: string;
  confirmPassword: string;
}

export interface CreateDoctorSceduleDataForm {
  dayOfWeek: string;
}

export interface CreateDoctorDayOffData {
  date: string;
  status: string;
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

export interface ResponseUser {
  data: UserData | null;
  status: number;
}

const getHeaders = (token?: string): Headers => {
  let headers = new Headers();

  headers.append("Content-Type", "application/json");
  headers.append("Accept", "application/json");
  headers.append("Access-Control-Allow-Origin", "http://localhost:5000");
  headers.append("Access-Control-Allow-Headers", "true");
  headers.append("Authorization", "bearer " + token);
  headers.append("Origin", "http://localhost:5000");

  return headers;
};

const addUserToAuthResult = async (
  userName: string,
  token: string,
): Promise<AuthResult> => {
  let headers = getHeaders(token);

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

export const getUserById = async (
  userId?: string,
  token?: string,
): Promise<ResponseUser> => {
  let headers = getHeaders(token);

  const userResponse = await fetch("http://localhost:5000/userById/" + userId, {
    mode: "cors",
    method: "GET",
    headers: headers,
  });

  if (userResponse.status === 401) return { data: null, status: 401 };
  else if (userResponse.status === 404) return { data: null, status: 404 };

  let userResult = await userResponse.json();

  return { data: userResult, status: 200 };
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

  if (result.token === "") return { success: true, token: "" };

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

  if (result.token === "") return { success: true, token: "" };

  return await addUserToAuthResult(patient.userName, result.token);
};

export const confirmUserEmail = async (
  email: string,
  token: string,
): Promise<boolean> => {
  let headers = getHeaders();

  const response = await fetch(
    "http://localhost:5000/confirmUserEmail?token=" + token + "&email=" + email,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 200) return true;

  return false;
};

export const resetPasswordRequest = async (email: string): Promise<string> => {
  let headers = getHeaders();

  const response = await fetch(
    "http://localhost:5000/resetPasswordRequest?email=" + email,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 200) return "";

  let result = await response.json();

  return result;
};

export const resetPassword = async (
  email: string,
  token: string,
  newPassword: string,
): Promise<boolean> => {
  let headers = getHeaders();

  const response = await fetch(
    "http://localhost:5000/resetPassword?token=" +
      token +
      "&email=" +
      email +
      "&newPassword=" +
      newPassword,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 200) return true;

  return false;
};

export const deleteUser = async (
  userName: string,
  token: string,
): Promise<boolean> => {
  let headers = getHeaders(token);

  const response = await fetch("http://localhost:5000/" + userName, {
    mode: "cors",
    method: "DELETE",
    headers: headers,
  });

  if (response.status === 204 || response.status === 401) return true;
  return false;
};
