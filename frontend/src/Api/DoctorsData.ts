export interface DoctorData {
  id: string;
  userId: string;
  firstName: string;
  lastName: string;
  speciality: string;
  licenseNo: string;
  workStart: Date;
  yearsOfExperience: number;
  workAddress: string;
  dateOfBurth: Date;
  zipCode: string;
  country: string;
  city: string;
}

export interface DoctorParameters {
  orderBy?: string;
  minExperienceYears?: number;
  fullNameSearchTerm?: string;
  specialitySearchTerm?: string;
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

export const getDoctors = async (
  token?: string,
  parameters?: DoctorParameters,
): Promise<DoctorData[]> => {
  let doctors: DoctorData[] = [];

  var query =
    parameters === undefined
      ? ""
      : "?" +
        JSON.stringify(parameters)
          .replaceAll("{", "")
          .replaceAll("}", "")
          .replaceAll(":", "=")
          .replaceAll('"', "");

  let headers = getHeaders(token);

  const response = await fetch("http://localhost:5000/api/doctors" + query, {
    mode: "cors",
    method: "GET",
    headers: headers,
  });

  if (response.status === 401) return [];

  doctors = await response.json();
  return doctors;
};

export const getDoctor = async (
  token?: string,
  doctorId?: string,
): Promise<DoctorData | null> => {
  let doctor = null;

  if (doctorId === undefined) return doctor;

  let headers = getHeaders(token);

  const response = await fetch(
    "http://localhost:5000/api/doctors/" + doctorId,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 401) return null;

  doctor = await response.json();

  return doctor;
};
