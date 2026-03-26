export const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5263';

import { fields as staticFields, Field } from '../data/fields';
import { products as staticProducts, Product } from '../data/products';

// Field interfaces mapped to backend
export interface FieldTrans {
  fieldTransId: number;
  langCode: string;
  name: string;
  description: string;
}

export interface BackendField {
  fieldId: number | string;
  nameField?: string;
  uid: string;
  image?: string;
  status: string;
  fieldTrans: FieldTrans[];
}

// Project/Product interfaces mapped to backend
export interface ProjectTrans {
  projectTransId: number;
  langCode: string;
  name: string;
  shortDescription?: string;
  description?: string;
}

export interface BackendProject {
  projectId: number | string;
  name?: string;
  description?: string;
  fieldId: number | string;
  image?: string;
  status: string;
  projectTrans: ProjectTrans[];
}

export interface ContactPayload {
  name: string;
  email: string;
  phone?: string;
  message: string;
}

export const api = {
  // Fetch active fields mapped from static
  getFields: async (): Promise<BackendField[]> => {
    return staticFields.map(f => ({
      fieldId: f.id,
      nameField: f.title,
      uid: f.id,
      image: f.image,
      status: 'active',
      fieldTrans: [{
        fieldTransId: 1,
        langCode: 'vi',
        name: f.title,
        description: f.description
      }],
      // Keep other attributes handy for components expecting them
      ...f
    })) as unknown as BackendField[];
  },

  // Fetch specific field by ID
  getFieldById: async (id: number | string): Promise<BackendField> => {
    const stringId = String(id);
    const f = staticFields.find(field => field.id === stringId);
    if (!f) throw new Error('Failed to fetch field');
    
    return {
      fieldId: f.id,
      nameField: f.title,
      uid: f.id,
      image: f.image,
      status: 'active',
      fieldTrans: [{
        fieldTransId: 1,
        langCode: 'vi',
        name: f.title,
        description: f.fullDescription
      }],
      ...f
    } as unknown as BackendField;
  },

  // Fetch active projects mapped from static products
  getProjects: async (): Promise<BackendProject[]> => {
    return staticProducts.map((p, index) => ({
      projectId: p.id,
      name: p.title,
      description: p.description,
      fieldId: p.fieldId,
      image: p.image,
      status: 'active',
      projectTrans: [{
        projectTransId: index,
        langCode: 'vi',
        name: p.title,
        shortDescription: p.description,
        description: p.fullDescription
      }],
      ...p
    })) as unknown as BackendProject[];
  },

  // Fetch specific project by ID
  getProjectById: async (id: number | string): Promise<BackendProject> => {
    const stringId = String(id);
    const p = staticProducts.find(product => product.id === stringId);
    if (!p) throw new Error('Failed to fetch project');

    return {
      projectId: p.id,
      name: p.title,
      description: p.fullDescription,
      fieldId: p.fieldId,
      image: p.image,
      status: 'active',
      projectTrans: [{
        projectTransId: 1,
        langCode: 'vi',
        name: p.title,
        shortDescription: p.description,
        description: p.fullDescription
      }],
      ...p
    } as unknown as BackendProject;
  },

  // Helper function to build full image URL (now mostly external images)
  getImageUrl: (path?: string) => {
    if (!path) return '';
    if (path.startsWith('http') || path.startsWith('data:')) return path;
    return `${API_BASE_URL}${path.startsWith('/') ? path : `/${path}`}`;
  },

  submitContact: async (data: ContactPayload) => {
    // Mocking contact submission for now
    return Promise.resolve({ success: true, message: "Thank you for contacting us." });
  }
};
