import { useState } from "react";
import { ButtonGroup } from "react-bootstrap";
import { PrimaryButton } from "../../../Styles/Common/Buttons";
import { Page } from "../../General/Page";
import { RegistrationDoctor } from "../Doctors/RegistrationDoctor";
import { RegistrationPatient } from "../Patients/RegistrationPatient";

export const RegistrationPage = () => {
  const [userType, setUserType] = useState("");
  return (
    <Page title="Зарегистрироваться как:">
      <ButtonGroup className="row d-flex justify-content-around">
        <PrimaryButton
          disabled={userType === "Doctor"}
          onClick={() => {
            setUserType("Doctor");
          }}
          className="col-4 row d-flex justify-content-center"
        >
          Доктор
        </PrimaryButton>
        <PrimaryButton
          disabled={userType === "Patient"}
          onClick={() => {
            setUserType("Patient");
          }}
          className="col-4 row d-flex justify-content-center"
        >
          Пациент
        </PrimaryButton>
      </ButtonGroup>
      {userType === "Doctor" && <RegistrationDoctor />}
      {userType === "Patient" && <RegistrationPatient />}
    </Page>
  );
};
