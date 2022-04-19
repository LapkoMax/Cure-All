export interface PatientData {
  id: string;
  patientCardId: string;
  userId: string;
  firstName: string;
  lastName: string;
  userName: string;
  phoneNumber: string;
  email: string;
  dateOfBurth: Date;
  zipCode: string;
  country: string;
  city: string;
}

export interface EditPatientForm {
  id: string;
  userId: string;
  firstName: string;
  lastName: string;
  oldUserName: string;
  userName: string;
  phoneNumber: string;
  email: string;
  dateOfBurth: string;
  zipCode: string;
  country: string;
  city: string;
  oldPassword: string;
  newPassword: string;
  confirmPassword: string;
}

export interface ResponsePatient {
  data: PatientData | null;
  responseStatus: number;
}

export interface ResponsePatients {
  data: PatientData[];
  responseStatus: number;
}

const getHeaders = (token?: string): Headers => {
  let headers = new Headers();

  headers.append("Content-Type", "application/json");
  headers.append("Accept", "application/json");
  headers.append("Access-Control-Allow-Origin", "http://localhost:5000");
  headers.append("Access-Control-Allow-Headers", "true");
  headers.append("Origin", "http://localhost:5000");
  headers.append("Authorization", "bearer " + token);

  return headers;
};

export const getPatient = async (
  token?: string,
  patientId?: string,
): Promise<ResponsePatient> => {
  let patient = null;

  if (patientId === undefined) return { data: patient, responseStatus: 404 };

  let headers = getHeaders(token);

  let response = await fetch(
    "http://localhost:5000/api/patients/" + patientId,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 401) return { data: patient, responseStatus: 401 };
  else if (response.status === 404)
    response = await fetch(
      "http://localhost:5000/api/patients/byUser/" + patientId,
      {
        mode: "cors",
        method: "GET",
        headers: headers,
      },
    );

  patient = await response.json();

  return { data: patient, responseStatus: 200 };
};

export const getPatientAmount = async (token?: string): Promise<number> => {
  let headers = getHeaders(token);

  let response = await fetch("http://localhost:5000/api/patients/amount", {
    mode: "cors",
    method: "GET",
    headers: headers,
  });

  if (response.status !== 200) return 0;

  var result = await response.json();

  return result;
};

export const editPatient = async (
  patient: EditPatientForm,
  token?: string,
): Promise<string[]> => {
  let headers = getHeaders(token);

  let response = await fetch(
    "http://localhost:5000/api/patients/" + patient.id,
    {
      mode: "cors",
      method: "PUT",
      body: JSON.stringify(patient),
      headers: headers,
    },
  );

  if (response.status === 401) return ["Unauthorized"];
  if (response.status === 200) return [];
  let result = await response.json();

  return result.errors !== null ? result.errors : [];
};
