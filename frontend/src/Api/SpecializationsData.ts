export interface SpecializationData {
  id: string;
  name: string;
  description: string;
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

export const getSpecializations = async (): Promise<SpecializationData[]> => {
  let headers = getHeaders();

  const response = await fetch("http://localhost:5000/api/specializations", {
    mode: "cors",
    method: "GET",
    headers: headers,
  });

  if (response.status !== 200) return [];

  let specializations = await response.json();

  return specializations;
};
