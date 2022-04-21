export interface DoctorData {
  id: string;
  userId: string;
  firstName: string;
  lastName: string;
  userName: string;
  phoneNumber: string;
  email: string;
  specialization: string;
  licenseNo: string;
  workStart: Date;
  yearsOfExperience: number;
  workAddress: string;
  averageAppointmentTime: number;
  workDayStart: string;
  workDayEnd: string;
  dinnerStart: string;
  dinnerEnd: string;
  doctorsScedule: DoctorSceduleData[];
  doctorDayOffs: DoctorDayOffData[];
  dateOfBurth: Date;
  zipCode: string;
  country: string;
  city: string;
}

export interface AvailableAppointmentTimeData {
  doctorId: string;
  time: string;
}

export interface DoctorSceduleData {
  id: string;
  doctorId: string;
  dayOfWeek: string;
}

export interface DoctorDayOffData {
  id?: string;
  doctorId?: string;
  date?: string;
  status: string;
  statusName: string;
}

export interface EditDoctorForm {
  id: string;
  userId: string;
  firstName: string;
  lastName: string;
  oldUserName: string;
  userName: string;
  phoneNumber: string;
  email: string;
  specializationId: string;
  licenseNo: string;
  workStart: string;
  workAddress: string;
  averageAppointmentTime: number;
  workDayStart: string;
  workDayEnd: string;
  dinnerStart: string;
  dinnerEnd: string;
  doctorsScedule: DoctorSceduleData[];
  doctorDayOffs: DoctorDayOffData[];
  dateOfBurth: string;
  zipCode: string;
  country: string;
  city: string;
  oldPassword: string;
  newPassword: string;
  confirmPassword: string;
}

export interface ResponseDoctor {
  data: DoctorData | null;
  responseStatus: number;
}

export interface ResponseDoctors {
  data: DoctorData[];
  responseStatus: number;
}

export interface ResponseAvailableTime {
  data: AvailableAppointmentTimeData[];
  responseStatus: number;
}

export interface DayOfWeek {
  id: number;
  value: string;
  name: string;
}

export const DaysOfWeek: DayOfWeek[] = [
  {
    id: 1,
    value: "Monday",
    name: "Понедельник",
  },
  {
    id: 2,
    value: "Tuesday",
    name: "Вторник",
  },
  {
    id: 3,
    value: "Wednesday",
    name: "Среда",
  },
  {
    id: 4,
    value: "Thursday",
    name: "Четверг",
  },
  {
    id: 5,
    value: "Friday",
    name: "Пятница",
  },
  {
    id: 6,
    value: "Saturday",
    name: "Суббота",
  },
  {
    id: 7,
    value: "Sunday",
    name: "Воскресенье",
  },
];

export const Statuses = [
  {
    id: 1,
    name: "Выходной",
    value: "DayOff",
  },
  {
    id: 2,
    name: "Больничный",
    value: "SickDay",
  },
  {
    id: 3,
    name: "Праздник",
    value: "Holiday",
  },
  {
    id: 4,
    name: "Отпуск",
    value: "Vacation",
  },
];

export interface DoctorFiend {
  id: number;
  name: string;
  displayName: string;
}

export const DoctorFields: DoctorFiend[] = [
  {
    id: 1,
    name: "lastname",
    displayName: "Фамилия",
  },
  {
    id: 2,
    name: "firstname",
    displayName: "Имя",
  },
  {
    id: 3,
    name: "workStart",
    displayName: "Опыт",
  },
  {
    id: 4,
    name: "country",
    displayName: "Страна",
  },
  {
    id: 5,
    name: "city",
    displayName: "Город",
  },
];

export interface DoctorParameters {
  orderBy?: string;
  minExperienceYears?: number;
  fullNameSearchTerm?: string;
  specialitySearchTerm?: string;
  countrySearchTerm?: string;
  citySearchTerm?: string;
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
): Promise<ResponseDoctors> => {
  let doctors: DoctorData[] = [];

  var query =
    parameters === undefined
      ? ""
      : "?" +
        JSON.stringify(parameters, function (key, value) {
          if (value === "" || value.toString().includes("&")) return undefined;
          return value;
        })
          .replaceAll("{", "")
          .replaceAll("}", "")
          .replaceAll(":", "=")
          .replaceAll('"', "")
          .replaceAll(",", "&");

  let headers = getHeaders(token);

  const response = await fetch("http://localhost:5000/api/doctors" + query, {
    mode: "cors",
    method: "GET",
    headers: headers,
  });

  if (response.status === 401) return { data: doctors, responseStatus: 401 };
  else if (response.status !== 200)
    return { data: doctors, responseStatus: response.status };

  doctors = await response.json();

  return { data: doctors, responseStatus: 200 };
};

export const getDoctor = async (
  token?: string,
  doctorId?: string,
): Promise<ResponseDoctor> => {
  let doctor = null;

  if (doctorId === undefined) return { data: doctor, responseStatus: 404 };

  let headers = getHeaders(token);

  let response = await fetch("http://localhost:5000/api/doctors/" + doctorId, {
    mode: "cors",
    method: "GET",
    headers: headers,
  });

  if (response.status === 401) return { data: doctor, responseStatus: 401 };
  else if (response.status === 404)
    response = await fetch(
      "http://localhost:5000/api/doctors/byUser/" + doctorId,
      {
        mode: "cors",
        method: "GET",
        headers: headers,
      },
    );

  doctor = await response.json();

  return { data: doctor, responseStatus: 200 };
};

export const getDoctorAvailableTime = async (
  doctorId: string,
  date: string,
  token?: string,
): Promise<ResponseAvailableTime> => {
  let headers = getHeaders(token);

  let response = await fetch(
    "http://localhost:5000/api/doctors/" +
      doctorId +
      "/availableTime?date=" +
      date,
    {
      mode: "cors",
      method: "GET",
      headers: headers,
    },
  );

  if (response.status === 401) return { data: [], responseStatus: 401 };
  else if (response.status !== 200)
    return { data: [], responseStatus: response.status };

  var results = await response.json();

  return { data: results, responseStatus: 200 };
};

export const getDoctorAmount = async (token?: string): Promise<number> => {
  let headers = getHeaders(token);

  let response = await fetch("http://localhost:5000/api/doctors/amount", {
    mode: "cors",
    method: "GET",
    headers: headers,
  });

  if (response.status !== 200) return 0;

  var result = await response.json();

  return result;
};

export const editDoctor = async (
  doctor: EditDoctorForm,
  token?: string,
): Promise<string[]> => {
  let headers = getHeaders(token);
  let response = await fetch("http://localhost:5000/api/doctors/" + doctor.id, {
    mode: "cors",
    method: "PUT",
    body: JSON.stringify(doctor),
    headers: headers,
  });

  if (response.status === 401) return ["Unauthorized"];
  if (response.status === 200) return [];
  let result = await response.json();

  return result.errors !== null ? result.errors : [];
};
