export interface IllnessData {
  id: string;
  name: string;
  description: string;
  symptoms: string;
}

export interface CreateIllnessForm {
  name: string;
  description: string;
  symptoms: string;
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

export const getIllneses = async (): Promise<IllnessData[]> => {
  let headers = getHeaders();

  const response = await fetch("http://localhost:5000/api/illneses", {
    mode: "cors",
    method: "GET",
    headers: headers,
  });

  if (response.status !== 200) return [];

  let illneses = await response.json();

  return illneses;
};

export const createNewIllness = async (
  illness: CreateIllnessForm,
  token?: string,
): Promise<string[]> => {
  let headers = getHeaders(token);

  const response = await fetch("http://localhost:5000/api/illneses", {
    mode: "cors",
    method: "POST",
    body: JSON.stringify(illness),
    headers: headers,
  });

  if (response.status === 401) return ["Unauthorized"];
  else if (response.status === 200) return [];

  let result = await response.json();

  return result.errors ? result.errors : [];
};
