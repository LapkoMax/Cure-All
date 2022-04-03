import { useEffect, useState } from "react";
import { DoctorData, getDoctors } from "../Api/DoctorsData";
import { DoctorList } from "./Doctors/DoctorList";

export const HomePage = () => {
  const [doctors, setDoctors] = useState<DoctorData[]>();

  useEffect(() => {
    const doGetDoctors = async () => {
      let resut = await getDoctors();
      setDoctors(resut);
    };
    doGetDoctors();
  }, []);

  return (
    <div>
      <div>
        <h2>Doctors</h2>
        <DoctorList data={doctors} />
        <button>Create new doctor</button>
      </div>
    </div>
  );
};
