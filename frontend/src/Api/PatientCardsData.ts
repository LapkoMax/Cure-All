export interface PatientCardData {
  id: string;
  patientId: string;
  patientUserId: string;
  patientFirstName: string;
  patientLastName: string;
}

export interface PatientCardResponse {
  data: PatientCardData | null;
  status: number;
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

export const getPatientCard = async (
  patientCardId?: string,
  token?: string,
): Promise<PatientCardResponse> => {
  let patientCard = null;

  let headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/patientCards/" + patientCardId,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 401) return { data: patientCard, status: 401 };
  else if (response.status !== 200)
    return { data: patientCard, status: response.status };

  patientCard = await response.json();

  return { data: patientCard, status: 200 };
};
