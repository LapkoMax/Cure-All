export interface IllnessData {
  id: string;
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
