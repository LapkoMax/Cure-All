/** @jsxImportSource @emotion/react */
import { useEffect, useState } from "react";
import { DoctorData, getDoctors } from "../Api/DoctorsData";
import { PrimaryButton } from "../Styles/Common/Buttons";
import { DoctorList } from "./Doctors/DoctorList";
import { Page } from "./General/Page";
import { PageTitle } from "./General/PageTitle";
import { titleContainer } from "../Styles/HomePageStyles";

export const HomePage = () => {
  const [doctors, setDoctors] = useState<DoctorData[]>();
  const [doctorsLoaded, setDoctorsLoaded] = useState(false);

  useEffect(() => {
    const doGetDoctors = async () => {
      let resut = await getDoctors();
      setDoctors(resut);
      setDoctorsLoaded(true);
    };
    doGetDoctors();
  }, []);

  return (
    <Page>
      <div css={titleContainer}>
        <PageTitle>Doctors</PageTitle>
        <PrimaryButton>Create new doctor</PrimaryButton>
      </div>
      {!doctorsLoaded ? <div>Loading...</div> : <DoctorList data={doctors} />}
    </Page>
  );
};
