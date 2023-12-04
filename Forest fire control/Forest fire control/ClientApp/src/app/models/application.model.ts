import { IncedentStatusEnum } from "./emums/incident-status.emum";
import { ObservationSite } from "./observation.model";

export interface Application {

    userEmail: string ;
    observationSite: ObservationSite;
    data: Date;
    description: string;
    status: IncedentStatusEnum;
  }