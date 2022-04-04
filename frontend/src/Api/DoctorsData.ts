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

export const getDoctors = async (
  parameters?: DoctorParameters,
): Promise<DoctorData[]> => {
  let doctors: DoctorData[] = [];

  let headers = new Headers();

  headers.append("Content-Type", "application/json");
  headers.append("Accept", "application/json");
  //headers.append('Authorization', 'Basic ' + base64.encode(username + ":" +  password));
  headers.append("Origin", "http://localhost:5000");

  var query =
    parameters === undefined
      ? ""
      : "?" +
        JSON.stringify(parameters)
          .replaceAll("{", "")
          .replaceAll("}", "")
          .replaceAll(":", "=")
          .replaceAll('"', "");

  console.log("http://localhost:5000/api/doctors" + query);

  const response = await fetch("http://localhost:5000/api/doctors" + query, {
    mode: "cors",
    //credentials: "include",
    method: "GET",
    headers: headers,
  });
  doctors = await response.json();
  return doctors;
};
